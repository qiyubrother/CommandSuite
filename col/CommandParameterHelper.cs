using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CommandParameterHelper
{

    public Dictionary<string, string> Parameters;
    public CommandParameterHelper(IEnumerable<string> args)
    {
        Parameters = new Dictionary<string, string>();
        foreach (var arg in args)
        {
            var pos =arg.IndexOf(':');
            if (pos < 0)
            {
                throw new Exception("Invalid Parameter.");
            }
            var key = arg.Substring(0, pos).ToLower();
            var val = arg.Substring(pos + 1);
            if (key[0] != '-' || key.Length < 2 || val.Length < 1) 
            {
                throw new Exception("Invalid Parameter.");
            }
            key = key.Substring(1);
            Parameters[key] = val;
        }
    }
}

