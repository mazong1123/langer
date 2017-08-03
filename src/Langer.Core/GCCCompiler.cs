using System;
using System.Diagnostics;
using System.IO;

namespace Langer.Core
{
    public class GCCCompliler : LangerCompiler
    {
        public GCCCompliler()
        {
            CompilerPath = "gcc";
        }

        public GCCCompliler(string compilerPath)
        {
            CompilerPath = compilerPath;
        }

        /// <summary>
        /// Compile a specific piece of C code.
        /// </summary>
        /// <param name="code">A piece of C code to compile</param>
        /// <returns>A ComplieResult object represents all compile time information.</returns>
        public override CompileResult Compile(string code)
        {
            string sourceFileNameWithoutExt = GenerateSourceFileNameWithoutExt();
            string sourceFilePath = GenerateSourceFile(sourceFileNameWithoutExt, code);
            string outputFilePath = GetOutputFilePath(sourceFileNameWithoutExt);

            Process compilerProcess = GenerateCompilerProcess(sourceFilePath, outputFilePath);
            compilerProcess.Start();

            string compileOutput = compilerProcess.StandardOutput.ReadToEnd();
            string errorOutput = compilerProcess.StandardError.ReadToEnd();
            compilerProcess.WaitForExit();

            // The output file will exist only if the compiling succeed.
            bool isSuccess = File.Exists(outputFilePath);

            CompileResult compileResult = new CompileResult() 
            {
                CompileOutput = compileOutput,
                ErrorOutput = errorOutput,
                BinaryFilePath = outputFilePath,
                IsSuccess = isSuccess
            };

            return compileResult;
        }

        protected Process GenerateCompilerProcess(string sourceFilePath, string outputFilePath)
        {
            string compilerOption = GetCompilerOption();

            // Prepare compiler process arguments.
            string arguments = string.IsNullOrWhiteSpace(compilerOption) ?
            string.Format("{0} -o {1}", sourceFilePath, outputFilePath)
            : string.Format("{0} {1} -o {2}", compilerOption, sourceFilePath, outputFilePath);

            Process compilerProcess = new Process
            {
                StartInfo =
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = CompilerPath,
                    Arguments = arguments
                }
            };

            return compilerProcess;
        }

        string GenerateSourceFileNameWithoutExt()
        {
            return Guid.NewGuid().ToString();
        }

        virtual protected string GetSourceFileExtention()
        {
            return ".c";
        }

        virtual protected string GetCompilerOption()
        {
            return string.Empty;
        }

        protected string GenerateSourceFile(string sourceFileNameWithoutExt, String code)
        {
            string sourceFileAbsolutePath = Path.Combine(TempCodeFolderPath, sourceFileNameWithoutExt + GetSourceFileExtention());
            using (StreamWriter sw = new StreamWriter(sourceFileAbsolutePath))
            {
                sw.Write(code);
            }

            return sourceFileAbsolutePath;
        }

        protected string GetOutputFilePath(string sourceFileNameWithoutExt)
        {
            return Path.Combine(TempCodeFolderPath, sourceFileNameWithoutExt + ".o");
        }

    }

}