using System.Collections.Generic;

public interface IStoreRepository {
    IEnumerable<Store> GetStores ();
   
}