using Microsoft.AspNetCore.Mvc;

namespace CarShopping.ApplicationService.Interfaces
{
    public interface ICarPartsAppService<TRequest, TEntity, TId>
    {
        //fazer um metodo padrao de criacao.
        Task<IActionResult> Create();
        Task<IActionResult> Update();
        Task<TEntity> Delete();
        Task<TEntity> Get(TId id);
        Task<IEnumerable<TEntity>> GetAll();

    }
}
