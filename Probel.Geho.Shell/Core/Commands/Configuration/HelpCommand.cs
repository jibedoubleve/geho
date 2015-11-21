namespace Probel.Geho.Shell.Core.Commands.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Probel.Geho.Shell.Helpers;

    [Command("help", "h", "Displays help")]
    public class HelpCommand : BaseCommand
    {
        #region Methods

        protected override void ExecuteCommand(IEnumerable<Argument> args)
        {
            if (!this.ArgProcessor.HasArgs) { DisplayHelp(); }
            else
            {
                var isShort = this.ArgProcessor.Has("-s");
                var cmd = this.ArgProcessor.Get("cmd");
                DisplayCommand(cmd, isShort);
            }
        }

        private static void DisplayHelp()
        {
            var types = GetTypes();

            foreach (var type in types)
            {
                var a = GetAttribute(type);

                Output.InWhite.Write("{0, -18} - {1, -5} - {2, -45} - ", a.Name, a.ShortName, a.Description);
                Output.InYellow.WriteLine(a.Pattern);
            }
        }

        private static CommandAttribute GetAttribute(Type type)
        {
            var a = (CommandAttribute)Attribute.GetCustomAttribute(type, typeof(CommandAttribute));
            return a;
        }

        private static IOrderedEnumerable<Type> GetTypes()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ICommand));
            var types = (from type in assembly.GetTypes()
                         where type.IsDefined(typeof(CommandAttribute), false)
                         select type).OrderBy(e => e.Name);
            return types;
        }

        private void DisplayCommand(string cmd, bool isShort)
        {
            cmd = cmd.ToLower();
            var types = GetTypes();

            foreach (var type in types)
            {
                var a = GetAttribute(type);
                if (a.Name == cmd || a.ShortName == cmd)
                {
                    Output.InWhite.WriteLine("Command     : {0} | {1}", a.Name, a.ShortName);
                    Output.InWhite.WriteLine("Description : {0}", a.Description);
                    Output.InYellow.WriteLine("Pattern     : {0}", a.Pattern);
                }
            }
        }

        #endregion Methods
    }
}