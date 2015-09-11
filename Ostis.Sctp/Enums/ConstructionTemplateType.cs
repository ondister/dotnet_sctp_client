namespace Ostis.Sctp
{
    internal enum ConstructionTemplateType : byte
    {
        Fixed_Assign_Assign = 0,
        Assign_Assign_Fixed = 1,
        Fixed_Assign_Fixed = 2,
        Fixed_Assign_Assign_Assign_Fixed = 3,
        Assign_Assign_Fixed_Assign_Fixed=4,
        Fixed_Assign_Fixed_Assign_Fixed=5,
        Fixed_Assign_Fixed_Assign_Assign=6,
        Fixed_Assign_Assign_Assign_Assign=7,
        Assign_Assign_Fixed_Assign_Assign=8
    }
}
