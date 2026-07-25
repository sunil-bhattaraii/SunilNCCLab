for i in {1..17}; do
(
    cd "../Lab-$i" && echo -e "\e[33m?\e[37mmain \e[32m~/B.Sc. CSIT/6th_sem/lab reports/SunilNCCLab/Lab-$i> dotnet \e[37mrun\e[0m" && dotnet run
)
done
