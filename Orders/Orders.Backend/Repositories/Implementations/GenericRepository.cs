using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.VisualBasic;
using Orders.Backend.Data;
using Orders.Backend.Helpers;
using Orders.Backend.Repositories.Interfaces;
using Orders.Shared.DTOs;
using Orders.Shared.Responses;

namespace Orders.Backend.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class //T es cualquier cosa que pasen
{
    //Se le inyecta el DataContext (inyección de dependencias)
    private readonly DataContext _context;

    //Es para interactuar con la colección de entidades del tipo T (valor que me pasan) en el contexto de datos.
    private readonly DbSet<T> _entity;

    //Constructor
    public GenericRepository(DataContext context)
    {
        _context = context;
        //Esto representa la colección de entidades del tipo T en el contexto de datos.
        _entity = _context.Set<T>();
    }

    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();

        return new ActionResponse<IEnumerable<T>>
        {
            WasSuccess = true,
            Result = await queryable
                .Paginate(pagination)
                .ToListAsync()
        };
    }

    public virtual async Task<ActionResponse<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _entity.AsQueryable();
        double count = await queryable.CountAsync();
        return new ActionResponse<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }

    //Devuelve un objeto de tipo ActionResponse<T> (clase que se creó para manejar respuestas)
    //Virtual para que se pueda sobreescribir en las clases que hereden de esta
    public virtual async Task<ActionResponse<T>> AddAsync(T entity)
    {
        //Agregar objeto a la BD
        _context.Add(entity);

        //Manejo de errores
        try
        {
            //Grabar cambios
            await _context.SaveChangesAsync();
            //Si se pudo guardar, devolver un ActionResponse con WasSuccess true y el objeto que se agregó
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity //Devuelvo el objeto que se agregó
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    //Método Delete, recibe un id y elimina la entidad correspondiente en la BD
    public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id); //Busca la entidad por su clave primaria (id)
        if (row == null)
        {
            //Devuelve un ActionResponse con WasSuccess false (no hay necesidad de ponerlo por ser false) y un mensaje de error
            return new ActionResponse<T>
            {
                Message = "Registro no encontrado."
            };
        }
        _entity.Remove(row); //Elimina la entidad encontrada
        try
        {
            //Grabar cambios si encontro la entidad
            await _context.SaveChangesAsync(); //Guarda los cambios en la BD
            return new ActionResponse<T>
            {
                WasSuccess = true,
            };
        }
        catch
        {
            return new ActionResponse<T>
            {
                Message = "No se pudo borrar porque tiene registros relacionados."
            };
        }
    }

    //Método Get con parámetro, devuelve un objeto de tipo T (cualquier cosa que me pasen)
    public virtual async Task<ActionResponse<T>> GetAsync(int id)
    {
        var row = await _entity.FindAsync(id); //Busca la entidad por su clave primaria (id)
        if (row == null)
        {
            //Devuelve un ActionResponse con WasSuccess false (no hay necesidad de ponerlo por ser false) y un mensaje de error
            return new ActionResponse<T>
            {
                Message = "Registro no encontrado."
            };
        }
        return new ActionResponse<T>
        {
            WasSuccess = true,
            Result = row //Devuelvo el objeto que se encontró
        };
    }

    //Método Get sin parámetro, devuelve una lista IEnumerable de objetos que yo pida
    public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync() => new ActionResponse<IEnumerable<T>>
    {
        WasSuccess = true,
        Result = await _entity.ToListAsync() // Devuelvo la lista de entidades
    };

    //Método Update, recibe una entidad y actualiza la información en la BD
    public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
    {
        //Actualizar objeto a la BD
        _context.Update(entity);

        //Manejo de errores
        try
        {
            //Grabar cambios
            await _context.SaveChangesAsync();

            //Si se pudo guardar, devolver un ActionResponse con WasSuccess true y el objeto que se actualizo
            return new ActionResponse<T>
            {
                WasSuccess = true,
                Result = entity //Devuelvo el objeto que se agregó
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    //En caso de que sea una excepción genérica devuelve un ActionResponse con el mensaje de error
    private ActionResponse<T> ExceptionActionResponse(Exception exception) => new ActionResponse<T>
    {
        //Para manejar errores generales
        Message = exception.Message
    };

    // Para manejar errores de clave duplicada
    private ActionResponse<T> DbUpdateExceptionActionResponse() => new ActionResponse<T>
    {
        Message = "Ya existe el registro."
    };
}