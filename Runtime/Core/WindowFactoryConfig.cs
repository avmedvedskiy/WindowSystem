using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UISystem
{
    [Serializable]
    public class WindowFactoryConfig
    {
        [SerializeField] private List<string> _buildInWindows;
        [SerializeField] private string _folderName;

        public List<string> BuildInWindows => _buildInWindows;
        public string FolderName => _folderName;

        public WindowFactoryConfig(List<string> buildInWindows, string folderName)
        {
            _buildInWindows = buildInWindows;
            _folderName = folderName;
        }
    }
}