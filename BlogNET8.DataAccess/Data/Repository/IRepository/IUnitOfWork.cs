using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogNET8.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        //Acá se agregan los distintos repositorios.
        ICategoryRepository CategoryRepository { get; }
		IArticleRepository ArticleRepository { get; }

		void Save();
    }
}
