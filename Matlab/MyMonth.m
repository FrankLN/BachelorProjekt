% Helper class Month, for finding number of days in a given month
classdef MyMonth < uint32
    enumeration
       January(1)
       February(2)
       March(3)
       April(4)
       May(5)
       June(6)
       July(7)
       August(8)
       September(9)
       October(10)
       November(11)
       December(12)
    end
    methods(Static)
        function r = getDays(month, year)
            switch month
                case uint32(MyMonth.January)
                    r = 31;
                case uint32(MyMonth.February)
                    if mod(year, 4) == 0
                        r = 29;
                    else
                        r = 28;
                    end
                case uint32(MyMonth.March)
                    r = 31;
                case uint32(MyMonth.April)
                    r = 30;
                case uint32(MyMonth.May)
                    r = 31;
                case uint32(MyMonth.June)
                    r = 30;
                case uint32(MyMonth.July)
                    r = 31;
                case uint32(MyMonth.August)
                    r = 31;
                case uint32(MyMonth.September)
                    r = 30;
                case uint32(MyMonth.October)
                    r = 31;
                otherwise
                    r = 0;
            end
        end
    end
end