namespace ProyectoFinalAPI_Antozzi.Repository.Interfaces

{
    public interface ICRUD <T>
    {
        T Add(T entity);
        List<T> GetAll();
        T Get(Int64 id);
        bool Update(T entity, Int64 id); 
        bool Delete(Int64 id);
        
    }
}
