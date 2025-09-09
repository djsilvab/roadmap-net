using System;

namespace Northwind.LibA;

public class HijaEnMismoProyecto : BaseClass
{
    public void Probar()
    {
        MetodoInternal();// ✅ permitido (mismo ensamblado)
        MetodoProtectedInternal(); // ✅ permitido (herencia + mismo ensamblado)
    }
}
