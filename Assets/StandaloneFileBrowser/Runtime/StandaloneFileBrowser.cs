using System;
using StandaloneFileBrowser.Runtime.Common;

#if UNITY_EDITOR
using StandaloneFileBrowser.Runtime.Editor;
#elif UNITY_STANDALONE_OSX
using StandaloneFileBrowser.Runtime.Mac;
#elif UNITY_STANDALONE_WIN
using StandaloneFileBrowser.Runtime.Windows;
#elif UNITY_STANDALONE_LINUX
using StandaloneFileBrowser.Runtime.Linux;
#endif

namespace StandaloneFileBrowser.Runtime
{
    public static class StandaloneFileBrowser
    {
        private static readonly IStandaloneFileBrowser PlatformWrapper;

        static StandaloneFileBrowser()
        {
#if UNITY_EDITOR
            PlatformWrapper = new StandaloneFileBrowserEditor();
#elif UNITY_STANDALONE_OSX
            PlatformWrapper = new StandaloneFileBrowserMac();
#elif UNITY_STANDALONE_WIN
            PlatformWrapper = new StandaloneFileBrowserWindows();
#elif UNITY_STANDALONE_LINUX
            PlatformWrapper = new StandaloneFileBrowserLinux();
#endif
        }

        /// <summary>
        /// Native open file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extension">Allowed extension</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
        public static string[] OpenFilePanel(string title, string directory, string extension,
            bool multiselect)
        {
            var extensions = string.IsNullOrEmpty(extension)
                ? null
                : new[] { new ExtensionFilter("", extension) };
            return OpenFilePanel(title, directory, extensions, multiselect);
        }

        /// <summary>
        /// Native open file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
        public static string[] OpenFilePanel(string title, string directory,
            ExtensionFilter[] extensions, bool multiselect)
        {
            return PlatformWrapper.OpenFilePanel(title, directory, extensions, multiselect);
        }

        /// <summary>
        /// Native open file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extension">Allowed extension</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <param name="cb">Callback")</param>
        public static void OpenFilePanelAsync(string title, string directory, string extension,
            bool multiselect, Action<string[]> cb)
        {
            var extensions = string.IsNullOrEmpty(extension)
                ? null
                : new[] { new ExtensionFilter("", extension) };
            OpenFilePanelAsync(title, directory, extensions, multiselect, cb);
        }

        /// <summary>
        /// Native open file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="multiselect">Allow multiple file selection</param>
        /// <param name="cb">Callback")</param>
        public static void OpenFilePanelAsync(string title, string directory,
            ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb)
        {
            PlatformWrapper.OpenFilePanelAsync(title, directory, extensions, multiselect, cb);
        }

        /// <summary>
        /// Native open folder dialog
        /// NOTE: Multiple folder selection doesn't supported on Windows
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory">Root directory</param>
        /// <param name="multiselect"></param>
        /// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
        public static string[] OpenFolderPanel(string title, string directory, bool multiselect)
        {
            return PlatformWrapper.OpenFolderPanel(title, directory, multiselect);
        }

        /// <summary>
        /// Native open folder dialog async
        /// NOTE: Multiple folder selection doesn't supported on Windows
        /// </summary>
        /// <param name="title"></param>
        /// <param name="directory">Root directory</param>
        /// <param name="multiselect"></param>
        /// <param name="cb">Callback")</param>
        public static void OpenFolderPanelAsync(string title, string directory, bool multiselect,
            Action<string[]> cb)
        {
            PlatformWrapper.OpenFolderPanelAsync(title, directory, multiselect, cb);
        }

        /// <summary>
        /// Native save file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extension">File extension</param>
        /// <returns>Returns chosen path. Empty string when cancelled</returns>
        public static string SaveFilePanel(string title, string directory, string defaultName,
            string extension)
        {
            var extensions = string.IsNullOrEmpty(extension)
                ? null
                : new[] { new ExtensionFilter("", extension) };
            return SaveFilePanel(title, directory, defaultName, extensions);
        }

        /// <summary>
        /// Native save file dialog
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <returns>Returns chosen path. Empty string when cancelled</returns>
        public static string SaveFilePanel(string title, string directory, string defaultName,
            ExtensionFilter[] extensions)
        {
            return PlatformWrapper.SaveFilePanel(title, directory, defaultName, extensions);
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extension">File extension</param>
        /// <param name="cb">Callback")</param>
        public static void SaveFilePanelAsync(string title, string directory, string defaultName,
            string extension, Action<string> cb)
        {
            var extensions = string.IsNullOrEmpty(extension)
                ? null
                : new[] { new ExtensionFilter("", extension) };
            SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
        }

        /// <summary>
        /// Native save file dialog async
        /// </summary>
        /// <param name="title">Dialog title</param>
        /// <param name="directory">Root directory</param>
        /// <param name="defaultName">Default file name</param>
        /// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
        /// <param name="cb">Callback")</param>
        public static void SaveFilePanelAsync(string title, string directory, string defaultName,
            ExtensionFilter[] extensions, Action<string> cb)
        {
            PlatformWrapper.SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
        }
    }
}