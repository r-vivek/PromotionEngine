using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace PromotionEngine
{
    public class JsonRulesReader : IRuleReader
    {
        public List<Rule> ReadRules()
        {
            return JsonConvert.DeserializeObject<List<Rule>>(
                File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"Rules.json")));
        }
    }
}
