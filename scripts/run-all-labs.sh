for i in {1..17}; do
(
    cd "../Lab-$i" && echo "?main ~/B.Sc. CSIT/6th_sem/lab reports/SunilNCCLab/Lab-$i> dotnet run" && dotnet run
)
done | tee all-outputs.txt
