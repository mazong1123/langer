using System;
using System.Collections.Generic;
using System.IO;

namespace Langer.Core
{
    public abstract class LangerCompiler
    {
        private string _tempCodeFolderPath = string.Empty;

        public string CompilerPath
        {
            get;
            set;
        }

        public string TempCodeFolderPath
        {
            get
            {
                return this._tempCodeFolderPath;
            }

            set
            {
                this._tempCodeFolderPath = value;

                // Ensure the tempCodeFolderPath end with "/" for Linux or "\" for Windows.
                if (!string.IsNullOrWhiteSpace(TempCodeFolderPath) &&
                value[value.Length - 1].Equals(Path.DirectorySeparatorChar))
                {
                    this._tempCodeFolderPath += Path.DirectorySeparatorChar;
                }
            }
        }

        public void Setup()
        {
            if (!Directory.Exists(TempCodeFolderPath))
            {
                Directory.CreateDirectory(TempCodeFolderPath);
            }
        }

        public abstract CompileResult Compile(string code);
    }
}
