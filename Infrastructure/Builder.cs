using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public abstract class Builder
    {
        // Метод, создающий продукт
        public abstract void CreateCryptographer(ICryptographer cryptographer);

        // Методы, которые строят части продукта
        public abstract void BuildKeyFilter(IFilter keyFilter);
        public abstract void BuildSourceFilter(IFilter keyFilter);
        public abstract void BuildValidator(IValidator validator);
        public abstract void BuildСrypter(ICrypter crypter);

        // Метод, возвращающий продукт клиенту
        public abstract ICryptographer GetCryptographer();
    }
}
