namespace Auth.Domain.Dtos;

public class PagingFilterRequest  
{
     public int PageNumber {get;set;}
     public int Limit {get;set;}
     public string SearchTerm {get;set;} = string.Empty;
}