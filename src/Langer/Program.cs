using System;
using Langer.Core;

namespace Langer
{
    class Program
    {
        static void Main(string[] args)
        {
            GCCCompliler compiler = new GCCCompliler();
            //compiler.TempCodeFolderPath = "~/lab/langeroutput";
            CompileResult cr = compiler.Compile("int main(){ return 0; }");

            Console.WriteLine(cr.IsSuccess);
            Console.WriteLine(cr.ErrorOutput);
            Console.WriteLine(cr.CompileOutput);
        }
    }
}
