namespace ProyectoFinalAPI_Antozzi.Repository.Interfaces

{
    public interface ICRUD <T>
    {
        T Add(T entity);
        List<T> GetAll();
        T Get(int id);
        bool Update(T entity, int id); 
        bool Delete(int id);
        
    }
}
