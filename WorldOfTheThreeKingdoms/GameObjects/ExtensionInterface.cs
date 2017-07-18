using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using GameObjects;
using GameGlobal;



public class ExtensionInterface
{
    private static Dictionary<String, String> extensionFiles = null;
    private static List<Type> compiledTypes = null;

    //private static void loadAllExtensionFiles()
    //{
    //    if (extensionFiles == null)
    //    {
    //        string[] filePaths;
    //        extensionFiles = new Dictionary<String, String>();
    //        try
    //        {
    //            filePaths = Directory.GetFiles("Content/Textures/Resources/Extensions/", "*.cs");
    //        }
    //        catch (DirectoryNotFoundException)
    //        {
    //            return;
    //        } 
    //        foreach (String fileName in filePaths)
    //        {
    //            TextReader tr = new StreamReader(fileName);
    //            String result = "";
    //            while (tr.Peek() >= 0)
    //            {
    //                result += tr.ReadLine() + "\n";
    //            }
    //            extensionFiles.Add(fileName, result);
    //            tr.Close();
    //        }
    //    }
    //}

    static string ProgramFilesx86()
    {
        if (8 == IntPtr.Size
            || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
        {
            return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
        }

        return Environment.GetEnvironmentVariable("ProgramFiles");
    }

    public static void loadCompiledTypes()
    {
        if (!GlobalVariables.EnableExtensions)
        {
            // extensions not enabled
            return;
        }
        /*
        if (compiledTypes == null)
        {
            compiledTypes = new List<Type>();
            loadAllExtensionFiles();
            TextWriter tw = null;
            try
            {
                tw = new StreamWriter("Content/Textures/Resources/Extensions/Errors.txt");
                foreach (KeyValuePair<String, String> file in extensionFiles)
                {
                    var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
                    var parameters = new CompilerParameters(new[] { "mscorlib.dll", "System.Core.dll", 
                        ProgramFilesx86() + @"/Microsoft XNA/XNA Game Studio/v3.0/References/Windows/x86/Microsoft.Xna.Framework.dll", 
                        "GameObjects.dll", "GameGlobal.dll" });
                    parameters.GenerateExecutable = false;
                    CompilerResults results = csc.CompileAssemblyFromSource(parameters, file.Value);
                    if (results.Errors.Count <= 0)
                    {
                        Type t = results.CompiledAssembly.GetModules()[0].GetTypes()[0];
                        compiledTypes.Add(t);
                    }
                    else
                    {
                        tw.WriteLine(">>> Cannot compile file " + file.Key);
                        foreach (CompilerError error in results.Errors)
                        {
                            tw.WriteLine(error.ErrorText);
                        }
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
            }
            finally
            {
                if (tw != null)
                {
                    tw.Dispose();
                }
            }
            try
            {
                File.Delete("Content/Textures/Resources/Extensions/RuntimeError.txt");
           } catch {}            
        }
        */
    }

    public static void call(String methodName, Object[] param)
    {
        if (!GlobalVariables.EnableExtensions)
        {
            // extensions not enabled
            return;
        }
        foreach (Type t in compiledTypes)
        {
            try
            {
                MethodInfo m = t.GetMethod(methodName);
                if (m != null)
                {
                    m.Invoke(Activator.CreateInstance(t), param);
                }
            }
            catch (Exception ex)
            {
                //StreamWriter w = null;
                //try
                //{
                //    w = File.AppendText("Content/Textures/Resources/Extensions/RuntimeError.txt");
                //    w.WriteLine(">>> In extension " + t.Name + " invoking " + methodName);
                //    w.WriteLine(ex.Message);

                //    Exception inner = ex.InnerException;
                //    while (inner != null)
                //    {
                //        w.WriteLine(inner.Message);
                //        inner = inner.InnerException;
                //    }
                //}
                //catch
                //{
                //}
                //finally
                //{
                //    if (w != null)
                //    {
                //        w.Dispose();
                //    }
                //}
            }
        }
    }

}
