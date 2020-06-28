using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine
{
    public interface IRuleReader
    {
        List<Rule> ReadRules();
    }
}
