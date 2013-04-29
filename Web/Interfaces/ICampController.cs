using System.Web.Mvc;


namespace Camp.Interfaces
{
    public interface ICampEntityController<T>
    {
        ActionResult Create();
        ActionResult Create(T entity);
        ActionResult Delete(int? id);
        ActionResult Details(int? id);
        ActionResult Edit(int? id);             
        ActionResult Edit(int? id, T entity);
        ActionResult Index();
    }
}
