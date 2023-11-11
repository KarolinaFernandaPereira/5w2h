using Bussines;
using Repository.Controle;
using Services.Validacao;

namespace Services
{
    public abstract class GenericServices<TEntity> where TEntity : class
    {
        private GenericRepository<TEntity> _repository;
        private GenericValidationDictionary _validationDictionary;

        public GenericServices(GenericRepository<TEntity> repository)
        {
            this._repository = repository;
            _validationDictionary = new ValidationDictionary();
        }

        public List<TEntity> BuscarTodos()
        {
            return _repository.BuscarTodos();
        }

        public virtual bool Criar(TEntity entity)
        {
            bool resp = false;
            if (entity != null)
            {
                _repository.Criar(entity);
                _repository.Salvar();
                return true;
            }
            return resp;
        }

        public bool Atualizar(TEntity entity)
        {
            bool resp = false;
            if (entity != null)
            {
                _repository.Atualizar(entity);
                _repository.Salvar();
                return true;
            }
            return resp;
        }

        
        public void Salvar()
        {
            _repository.Salvar();
        }

        internal GenericRepository<TEntity> Repository
        {
            get { return _repository; }
        }

        public GenericValidationDictionary ValidationDictionary { get { return _validationDictionary; } } 

    }
}