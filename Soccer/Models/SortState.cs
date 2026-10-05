namespace Soccer.Models;

public enum SortState
{
    NameAsc,      // за ім'ям гравця (зростання)
    NameDesc,     // за ім'ям гравця (спадання)
    AgeAsc,       // за віком (зростання)
    AgeDesc,      // за віком (спадання)
    PositionAsc,  // за позицією (зростання)
    PositionDesc, // за позицією (спадання)
    TeamAsc,      // за клубом (зростання)
    TeamDesc      // за клубом (спадання)
}