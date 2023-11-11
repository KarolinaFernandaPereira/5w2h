using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Validacao
{
    internal class ValidationDictionary : GenericValidationDictionary
    {

        private Dictionary<string, List<string>> _dicionario;

        public ValidationDictionary() { 
            _dicionario = new Dictionary<string, List<string>>();
        }

        public void AddError(string key, string mensagemErro)
        {
            if (!_dicionario.ContainsKey(key))
            {
                _dicionario[key] = new List<string>();
            }
            _dicionario[key].Add(mensagemErro);
        }

        public bool isValid
        {
            get
            {
                return !(_dicionario.Count > 0);
            }
        }

        public Dictionary<string, List<string>> errors
        {
            get
            {
                return _dicionario;
            }
        }

    }
}
