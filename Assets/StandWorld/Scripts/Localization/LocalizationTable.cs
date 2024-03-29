#region License
// ====================================================
// Project Porcupine Copyright(C) 2016 Team Porcupine
// This program comes with ABSOLUTELY NO WARRANTY; This is free software, 
// and you are welcome to redistribute it under certain conditions; See 
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace ProjectPorcupine.Localization
{
    /// <summary>
    /// The central class containing localization information.
    /// </summary>
    public static class LocalizationTable
    {
        // The current language. This will be automatically be set by the LocalizationLoader.
        // Default is English.
        public static string currentLanguage = DefaultLanguage;

        // The hash for the current localization code
        public static string hash = string.Empty;    // Set to something to ensure will be downloaded if not available.

        // Used by the LocalizationLoader to ensure that the localization files are only loaded once.
        public static bool initialized = false;

        public static JToken protoJson;

        private static readonly string DefaultLanguage = "uk_UA";

        // Contains basic information about each localization
        private static Dictionary<string, LocalizationData> localizationConfigurations;

        // The dictionary that stores all the localization values.
        private static Dictionary<string, Dictionary<string, string>> localizationTable = new Dictionary<string, Dictionary<string, string>>();

        // Does the config exists? Initally assumes true. Used to silence repetitive errors
        private static bool configExists = true;

        // Keeps track of what keys we've already logged are missing.
        private static HashSet<string> missingKeysLogged = new HashSet<string>();

        public static event Action CBLocalizationFilesChanged;

        public enum FallbackMode
        {
            ReturnKey, ReturnDefaultLanguage
        }

        /// <summary>
        /// Load a localization file from the harddrive.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        public static void LoadLocalizationFile(string path)
        {
            string localizationCode = Path.GetFileNameWithoutExtension(path);
            LoadLocalizationFile(path, localizationCode);
        }

        /// <summary>
        /// Returns the localization for the given key, or the key itself, if no translation exists.
        /// </summary>
        /// <param name="key">The key that should be searched for.</param>
        /// <param name="additionalValues">The values that should be inserted.</param>
        /// <returns></returns>
        public static string GetLocalization(string key, params object[] additionalValues)
        {
            // Return the localization of the advanced method.
            return GetLocalization(key, FallbackMode.ReturnDefaultLanguage, currentLanguage, additionalValues);
        }

        /// <summary>
        /// Returns the localization for the given key, or the key itself, if no translation exists.
        /// </summary>
        public static string GetLocalization(string key, FallbackMode fallbackMode, string language, params object[] additionalValues)
        {
            string value;

            if (additionalValues != null)
            {
                for (int i = 0; i < additionalValues.Length; i++)
                {
                    if (additionalValues[i] is string)
                    {
                        additionalValues[i] = GetLocalization(additionalValues[i].ToString(), fallbackMode, language);
                    }
                }
            }

            Dictionary<string, string> languageTable;
            if (localizationTable.TryGetValue(language, out languageTable) && languageTable.TryGetValue(key, out value))
            {
                try
                {
                    return string.Format(value, additionalValues);
                }
                catch (FormatException)
                {
                    Debug.LogWarning("Bad localization for " + key + ". Arguments found: " + additionalValues.Length);
                }
            }

            // If add returns true the key was not present in the HashSet (i.e we just added it and wasn't there before)
            if (key != string.Empty && IsNumber(key) && missingKeysLogged.Add(key))
            {
                UnityDebugger.Debugger.LogWarning("LocalizationTable", string.Format("Translation for {0} in {1} language failed: Key not in dictionary.", key, language));
            }

            switch (fallbackMode)
            {
                case FallbackMode.ReturnKey:
                    return additionalValues != null && additionalValues.Length >= 1 ? key + " " + additionalValues[0] : key;
                case FallbackMode.ReturnDefaultLanguage:
                    return GetLocalization(key, FallbackMode.ReturnKey, DefaultLanguage, additionalValues);
                default:
                    return string.Empty;
            }
        }

        public static string GetLocalizaitonCodeLocalization(string code)
        {
            LocalizationData localizationData;
            if (localizationConfigurations.TryGetValue(code, out localizationData) == false)
            {
                UnityDebugger.Debugger.Log("LocalizationTable", "name of " + code + " is not present in config file.");
                return code;
            }

            return localizationData.LocalName;
        }

        public static void SetLocalization(int lang)
        {
            string[] languages = GetLanguages();
            currentLanguage = languages[lang];
            //Settings.SetSetting("localization", languages[lang]);
            LocalizationLoader loader = GameObject.Find("GameContoller").GetComponent(typeof(LocalizationLoader)) as LocalizationLoader;
            loader.UpdateLocalizationTable();
        }

        public static void LoadingLanguagesFinished()
        {
            initialized = true;

            // C# 6 Support pls ;_;
            if (CBLocalizationFilesChanged != null)
            {
                CBLocalizationFilesChanged();
            }
        }

        public static void SaveConfigFile(string latestCommitHash, string filePath)
        {
            protoJson["version"] = latestCommitHash;
            hash = latestCommitHash;
            StreamWriter sw = new StreamWriter(filePath);
            JsonWriter writer = new JsonTextWriter(sw);

            // Launch saving operation in a separate thread.
            // This reduces lag while saving by a little bit.
            Thread t = new Thread(new ThreadStart(delegate { SaveJsonToHdd(protoJson, writer); }));
            t.Start();
        }

        /// <summary>
        /// Gets all languages present in library.
        /// </summary>
        public static string[] GetLanguages()
        {
            return localizationTable.Keys.ToArray();
        }

        public static void LoadConfigFile(string pathToConfigFile)
        {
            localizationConfigurations = new Dictionary<string, LocalizationData>();
            try
            {
                if (File.Exists(pathToConfigFile) == false)
                {
                    UnityDebugger.Debugger.LogError("LocalizationTable", "No config file found at: " + pathToConfigFile);
                    configExists = false;
                    return;
                }

                StreamReader reader = File.OpenText(pathToConfigFile);
                protoJson = JToken.ReadFrom(new JsonTextReader(reader));
                reader.Close();

                hash = protoJson["version"].ToString();

                if (protoJson["languages"] != null)
                {
                    foreach (JToken item in protoJson["languages"])
                    {
                        string code = item["code"].ToString();
                        string localName = code;
                        bool rtl = false;
                        try
                        {
                            rtl = bool.Parse(item["rtl"].ToString());
                        }
                        catch (NullReferenceException)
                        {
                        }

                        try
                        {
                            localName = item["name"].ToString();
                        }
                        catch (NullReferenceException)
                        {
                        }

                        localizationConfigurations.Add(code, new LocalizationData(code, localName, rtl));
                    }
                }
            }
            catch (Exception e)
            {
                UnityDebugger.Debugger.LogWarning("LocalizationTable", e);
            }
            }

        /// <summary>
        /// Reverses the order of characters in a string. Used for Right to Left languages, since UI doesn't do so automatically.
        /// </summary>
        /// <param name="original">The original and correct RTL text.</param>
        /// <returns>The string with the order of the characters reversed.</returns>
        public static string ReverseString(string original)
        {
            if (original == null)
            {
                return null;
            }

            char[] letterArray = original.ToCharArray();
            Array.Reverse(letterArray);
            string reverse = new string(letterArray);
            string[] revArray = reverse.Split(new char[] { '}', '{' });

            int throwAway;
            for (int i = 0; i < revArray.Length; i++)
            {
                // No brackets found, so just skip parsing them
                if (revArray.Length == 1)
                {
                    break;
                }

                if (int.TryParse(revArray[i], out throwAway))
                {
                    // This is the middle of a {#} segment of the string so let's add back the {} in the correct order for the parser
                    // Note: revArray[i] is passed through ReverseString again so that the numbers digits order is flipped back to LTR
                    revArray[i] = "{" + ReverseString(revArray[i]) + "}";
                }
                else
                {
                    // For now lets assume that passing in { or } without a number in between is likely an error
                    // why would a string need curly brackets in game?
                    // Note: this removes the curly braces and cannot replace them since string.split doesn't say whether { or } appeared
                    UnityDebugger.Debugger.LogWarning("LocalizationTable", "{ or } exist in localization string '" + original + "' for " + currentLanguage + "but do not enclose a number for string substitution.");
                }
            }

            // rebuild the reversed string
            return string.Join(null, revArray);
        }

        /// <summary>
        /// Destroy all recorded Delegates when changing scenes.
        /// </summary>
        public static void UnregisterDelegates()
        {
            CBLocalizationFilesChanged = null;
        }

        /// <summary>
        /// Checks to see if an object is a number.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsNumber(object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        /// <summary>
        /// Load a localization file from the harddrive with a defined localization code.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="localizationCode">The localization code, e.g.: "en_US", "en_UK".</param>
        private static void LoadLocalizationFile(string path, string localizationCode)
        {
            try
            {
                Dictionary<string, string> localeTable;
                if (localizationTable.TryGetValue(localizationCode, out localeTable) == false)
                {
                    localeTable = new Dictionary<string, string>();
                    localizationTable[localizationCode] = localeTable;
                }

                LocalizationData localizationConfig = null;
                if (configExists && localizationConfigurations.TryGetValue(localizationCode, out localizationConfig) == false)
                {
                    localizationConfig = null;
                    UnityDebugger.Debugger.LogError("LocalizationTable", "Language: " + localizationCode + " not defined in localization/config.json");
                }

                // Only the current and default languages translations will be loaded in memory.
                if (localizationCode == DefaultLanguage || localizationCode == currentLanguage)
                {
                    bool rightToLeftLanguage;
                    if (localizationConfig == null)
                    {
                        UnityDebugger.Debugger.LogWarning("LocalizationTable", "Assuming " + localizationCode + " is LTR");
                        rightToLeftLanguage = false;
                    }
                    else
                    {
                        rightToLeftLanguage = localizationConfig.isRightToLeft;
                    }

                    string[] lines = File.ReadAllLines(path);

                    foreach (string line in lines)
                    {
                        if (line.Length < 1 || line[0] == '#')
                        {
                            continue;
                        }

                        string[] keyValuePair = line.Split(new char[] { '=' }, 2);

                        if (keyValuePair.Length != 2)
                        {
                            UnityDebugger.Debugger.LogError("LocalizationTable", string.Format("Invalid format of localization string. Actual {0}", line));
                            continue;
                        }

                        if (rightToLeftLanguage)
                        {
                            // reverse order of letters in the localization string since unity UI doesn't support RTL languages
                            keyValuePair[1] = ReverseString(keyValuePair[1]);
                        }

                        localeTable[keyValuePair[0]] = keyValuePair[1];
                    }
                }
            }
            catch (FileNotFoundException exception)
            {
                UnityDebugger.Debugger.LogError("LocalizationTable", new Exception(string.Format("There is no localization file for {0}", localizationCode), exception).ToString());
            }
        }

        private static void SaveJsonToHdd(JToken json, JsonWriter writer)
        {
            JsonSerializer serializer = new JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
            serializer.Serialize(writer, json);

            writer.Flush();
            writer.Close();
        }
    }
}
