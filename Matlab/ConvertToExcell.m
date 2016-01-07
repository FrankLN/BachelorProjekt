function ConvertToExcell()
    filename = 'convertetData.xlsx';
    data = {'Transmission ID', 'Patient first name', 'Patient last name', 'DateOfBirth', 'Date', 'Episode names', 'Battery percent', 'Battery date'};
    
    %data = [data ; {1, 'test', '20000000000000', 'test', 100}]
    
    directory_name = 'FilesToConvert';
    files = dir(directory_name);

    fileIndex = find(~[files.isdir]);

    for i = 1:length(fileIndex)
        fileName = files(fileIndex(i)).name;
        path = strcat(directory_name, '/' ,fileName);
        
        patientFirstName = '';
        patientLastName = '';
        dateOfBirth = '';
        date = '';
        episodeNames = '';
        batteryPercent = 0;
        batteryDate = '';

        fid = fopen(path);
        tline = fgetl(fid);
        while ischar(tline)
            if findstr(tline, 'PID')
                A = findstr(tline, '|');
                dateOfBirth = tline(A(7)+1:A(8)-1);
                patientName = tline(A(5)+1:A(6)-1);
                A = findstr(patientName, '^');
                patientFirstName = patientName(1:A(1)-1)
                patientLastName = patientName(A(1)+1:end)
                
            end
            if findstr(tline, 'OBR|1')
                A = findstr(tline, '|');
                date = tline(A(7)+1:A(8)-1);
            end
            if findstr(tline, '737952')
                A = findstr(tline, '|');
                episodeNames = strcat(episodeNames, '|', tline(A(5)+1:A(6)-1), '|', tline(A(14)+1:end), '|');
            end
            if findstr(tline, '721344')
                A = findstr(tline, '|');
                disp(tline(A(5)+1:A(6)-1));
                batteryPercent = tline(A(5)+1:A(6)-1);
                batteryDate = tline(A(14)+1:end);
            end
            tline = fgetl(fid);
        end
        fclose(fid);
        disp(' ')
        
        data = [data ; {i, patientFirstName, patientLastName, dateOfBirth, date, episodeNames, batteryPercent, batteryDate}]
    end
    xlswrite(filename, data);
end