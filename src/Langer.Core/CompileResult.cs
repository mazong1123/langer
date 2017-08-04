using System;

namespace Langer.Core
{
    public struct CompileResult
    {
        public string CompileOutput
        {
            get;
            set;
        }

        public string ErrorOutput
        {
            get;
            set;
        }

        public string BinaryFilePath
        {
            get;
            set;
        }

        public bool IsSuccess
        {
            get;
            set;
        }

        public bool IsTimeout
        {
            get;
            set;
        }

        public TimeSpan CompileTime
        {
            get;
            set;
        }
    }
}