public class DependencyBase<T>
{
  private static int id = 0;

  public DependencyBase()
  {
    id++;
  }

  public int GetId() => id;
}

public class SingletonDep : DependencyBase<SingletonDep> { }
public class ScopedDep : DependencyBase<ScopedDep> { }
public class TransientDep : DependencyBase<TransientDep> { }
