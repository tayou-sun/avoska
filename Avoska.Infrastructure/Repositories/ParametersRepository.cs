using System.Collections.Generic;
using System.Linq;
using CStuffControl.Infrastructure;

public class ParametersRepository : IParametersRepository
{
    private AppDbContext appDbContext;
    public ParametersRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    public List<Parameters> GetAll()
    {
        var res = this.appDbContext.Parameters.ToList();
        return res;
    }
}