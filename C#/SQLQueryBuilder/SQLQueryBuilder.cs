namespace SQLQueryBuilder;

// todo add JOINS and Range queries and OR's
// todo add Range queries
// todo add OR's
public class SQLQueryBuilder
{
    private List<string> selectedColumns;
    private List<FilterClause> filters;
    private string tableName;
    private int offset;
    private int limit;

    public SQLQueryBuilder()
    {
        this.selectedColumns = new List<string>();
        this.filters = new List<FilterClause>();
        this.offset = -1;
        this.limit = -1;
    }

    public SQLQueryBuilder SELECT(params string[] selectedColumns)
    {
        foreach (string column in selectedColumns)
        {
            this.selectedColumns.Add(column);
        }
        return this;
    }
    public SQLQueryBuilder FROM(string tableName)
    {
        this.tableName = tableName;
        return this;
    }

    public SQLQueryBuilder WHERE(params FilterClause[] filters)
    {
        foreach (var filter in filters)
        {
            this.filters.Add(filter);
        }

        return this;
    }

    public SQLQueryBuilder OFFSET(int offset)
    {
        this.offset = offset;
        return this;
    }

    public SQLQueryBuilder LIMIT(int limit)
    {
        this.limit = limit;
        return this;
    }
    public string build()
    {
      
        if (selectedColumns.Count == 0)
        {
            throw new Exception("Must specify columns OR (*) ");
        }
        if (tableName.Length == 0)
        {
            throw new Exception("Table Name Not Specified");
        }
        string query = "";
        query += "SELECT ";
        for (int i = 0; i < selectedColumns.Count; i++)
        {
            query += selectedColumns[i];
            if (i != selectedColumns.Count - 1)
            {
                query += ", ";
            }
            else
            {
                query += " ";
            }
        }
        
        query += "FROM ";
        query += tableName;
        query += " ";

        if (filters.Count != 0)
        {
            query += "WHERE ";
            for (int i = 0; i < filters.Count; i++)
            {
                query += filters[i].buildPredicate();
                if (i != filters.Count - 1)
                {
                    query += " AND ";
                }
                else
                {
                    query += " ";
                }
            }
            
        }

        if (offset > 0)
        {
            query += "OFFSET ";
            query += offset.ToString();
            query += " ";
        }

        if (limit > 0)
        {
            query += "LIMIT ";
            query += limit.ToString();
            query += " ";

        }
        return query;
    }
}

public class FilterClause
{
    private readonly string columnToBeFiltered;
    private readonly string condition;
    private readonly string valueToBeComparedAgainst;

    public FilterClause(string columnToBeFiltered,string condition,string valueToBeComparedAgainst)
    {
        this.columnToBeFiltered = columnToBeFiltered;
        this.condition = condition;
        this.valueToBeComparedAgainst = valueToBeComparedAgainst;
    }

    internal string buildPredicate()
    {
        return this.columnToBeFiltered + " " + this.condition +" "+ this.valueToBeComparedAgainst ;
    }
}