using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

using e_library.EFC_Database.Context;
using e_library.EFC_Database.Entities;
using e_library.Model;

namespace e_library.Model;


public sealed record AuthorInfo
{
  public int ID { get; init; }
  
	public string Name { get; init; }
	
  public static AuthorInfo FromAuthorEntity(Author author)
	{
	  return new AuthorInfo
	  {
	    ID = author.ID,
	    Name = author.Name
	  };
	}
	
	public Author ToAuthorEntity(DBContext context)
	{
	  var author = context.Authors.FirstOrDefault(a => Name == a.Name);
    
	  return author ?? new Author { Name = Name };
	}
	
	public bool Exists(DBContext context)
	{
	  return context.Authors.FirstOrDefault(a => a.Name == Name) is not null;
	}
	
	public bool Equals(Author a)
	{
    if (a.Name == Name)
      return true;
    else
      return false;
	}
}

