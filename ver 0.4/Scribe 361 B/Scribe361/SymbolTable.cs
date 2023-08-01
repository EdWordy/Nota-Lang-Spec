namespace Scribe361
{
    public class Symbol
    {
        public string Name { get; }
        public string Type { get; }
        public object Value { get; set; }

        public Symbol(string name, string type, object value = null)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }

    public class SymbolTable
    {
        private Dictionary<string, Symbol> _symbols = new Dictionary<string, Symbol>();

        public SymbolTable()
        {
            // keywords
            Symbol intKeyword = new Symbol("intKeyword", "intKeyword", "int");
            Insert(intKeyword);
            Symbol strKeyword = new Symbol("strKeyword", "strKeyword", "str");
            Insert(strKeyword);
            Symbol boolKeyword = new Symbol("boolKeyword", "boolKeyword", "bool");
            Insert(boolKeyword);
            Symbol func = new Symbol("func", "func");
            Insert(func);
            Symbol keyword = new Symbol("keyword", "keyword");
            Insert(keyword);
            // control structures
            Symbol ifStatement = new Symbol("ifStatement", "ifStatement");
            Insert(ifStatement);
            // operators
            Symbol plusOp = new Symbol("plusOperator", "plusOperator", "+");
            Insert(plusOp);
            Symbol minusOp = new Symbol("minusOperator", "minusOperator", "-");
            Insert(minusOp);
            Symbol equalsOp = new Symbol("equalsOperator", "equalsOperator", "=");
            Insert(equalsOp);
            Symbol multiplyOp = new Symbol("multiplyOperator", "multiplyOperator", "*");
            Insert(multiplyOp);
            Symbol divideOp = new Symbol("divideOperator", "divideOperator", "/");
            Insert(divideOp);
        }

        // - - - - - - - -
        // helper methods
        // - - - - - - - -

        public void Insert(Symbol symbol)
        {
            if (_symbols.ContainsKey(symbol.Name))
            {
                throw new InvalidOperationException($"Symbol '{symbol.Name}' is already defined");
            }
            _symbols[symbol.Name] = symbol;
        }

        public Symbol Lookup(string name)
        {
            if (!_symbols.TryGetValue(name, out Symbol symbol))
            {
                throw new InvalidOperationException($"Symbol '{name}' is not defined");
            }
            return symbol;
        }

        public void Remove(string name)
        {
            if (!_symbols.ContainsKey(name))
            {
                throw new InvalidOperationException($"Symbol '{name}' is not defined");
            }
            _symbols.Remove(name);
        }
    }
}
