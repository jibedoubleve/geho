namespace Probel.Geho.Shell.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Probel.Geho.Shell.Core.Commands;
    using Probel.Geho.Shell.Helpers;

    public class CommandShell
    {
        #region Methods

        public void Start(params string[] quitCommands)
        {
            var cmdLine = string.Empty;

            Console.WriteLine("Schelule manager");
            Console.WriteLine("----------------");

            while (true)
            {
                try
                {
                    Console.Write("> ");
                  cmdLine = Console.ReadLine();

                    if (string.IsNullOrEmpty(cmdLine)) { continue; }
                    else if (quitCommands.Contains(cmdLine)) { break; }

                    var lines = cmdLine.Split(' ');
                    var command = lines.ElementAt(0);

                    cmdLine = string.Empty;
                    for (int i = 1; i < lines.Length; i++) { cmdLine += lines[i]; }

                    var cmd = GetCommand(command);
                    if (cmd == null) { Output.InYellow.WriteLine("Command not found"); }
                    else
                    {
                        var args = GetArguments(cmdLine);
                        cmd.Execute(args);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Output.InRed.WriteLine(ex.Message);
                }

            }
            Output.InYellow.WriteLine("Bye...");
        }

        private IEnumerable<Argument> GetArguments(string cmdLine)
        {
            var arguments = cmdLine.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            var argList = new List<Argument>();

            foreach (var argument in arguments)
            {
                var splitedArgs = argument.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                var arg1 = (splitedArgs.Count() >= 1)
                    ? splitedArgs.ElementAt(0)
                    : string.Empty;

                var arg2 = (splitedArgs.Count() == 2)
                    ? splitedArgs.ElementAt(1)
                    : string.Empty;

                argList.Add(new Argument(arg1, arg2));
            }

            return argList;
        }

        private ICommand GetCommand(string cmdLine)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ICommand));

            var types = (from type in assembly.GetTypes()
                         where type.IsDefined(typeof(CommandAttribute), false)
                         select type);

            cmdLine = cmdLine.ToLower();
            foreach (var type in types)
            {
                var a = (CommandAttribute)Attribute.GetCustomAttribute(type, typeof(CommandAttribute));

                if (a.ShortName == cmdLine || a.Name == cmdLine)
                {
                    return (ICommand)Activator.CreateInstance(type);
                }
            }

            return null;
        }

        #endregion Methods
    }
}