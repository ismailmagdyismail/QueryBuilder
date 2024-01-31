// See https://aka.ms/new-console-template for more information


using SQLQueryBuilder;

string q1 =
    new SQLQueryBuilder.SQLQueryBuilder()
        .SELECT("col1", "col2", "col3")
        .FROM("table1")
        .WHERE(new FilterClause("col1", ">=", "10"))
        .OFFSET(10)
        .LIMIT(20).build();
Console.WriteLine(q1);


string q2 =
    new SQLQueryBuilder.SQLQueryBuilder()
        .SELECT("col1", "col2", "col3")
        .FROM("table1")
        .WHERE(new FilterClause("col1", ">=", "10"),new FilterClause("col2","is","NULL"))
        .build();
Console.WriteLine(q2);
