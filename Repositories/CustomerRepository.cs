using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUI.Repositories;

public interface ICustomerRepository
{
    Task AddCustomer(Customer customer);
    Task UpdateCustomer(Customer customer);
    Task<Customer?> GetCustomerById(int id);
    Task DeleteCustomer(Customer customer);
    Task<IEnumerable<Customer>> GetCustomers();
}
public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;
    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCustomer(Customer customer)
    {
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    
}
