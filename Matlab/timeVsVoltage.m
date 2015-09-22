% Generated data for one person with time stamps and voltage values.
filename = 'timeVsVoltage.xlsx';

implantDate = Date('20110101000000');

T = [str2num(implantDate.toString())];

i = 1;
while(i < 10)
    j = 99;
    while (100*rand(1)) < j
        implantDate = implantDate.addHours(12);
        j = j-1;
    end
    T = [T ;str2num(implantDate.toString())];
    i = i + 1;
end

T

A = {'Time'; T};

xlswrite(filename,T,'Sheet1','A1');
