for i in {4..17}; do
(
    cd "../Lab-$i" || exit

    cat > Program.cs <<EOF
namespace Lab$i;

class Program
{
    static void Main()
    {
        Shared.Print.MyDetails($i);
    }
}
EOF
)
done
