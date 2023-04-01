using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribe361
{
    public class SymbolTable
    {
        private Dictionary<string, AstNode> _symbols;

        public SymbolTable()
        {
            _symbols = new Dictionary<string, AstNode>();
        }

        public void Define(string name, AstNode node)
        {
            _symbols[name] = node;
        }

        public AstNode Lookup(string name)
        {
            if (_symbols.TryGetValue(name, out AstNode node))
            {
                return node;
            }

            return null;
        }
    }
}
