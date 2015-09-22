% Class Date
classdef Date
    properties
        day
        month
        year
        hour
        minute
        second
    end
    methods
        function obj = Date(varargin)
            if nargin > 1
                obj.year = num2str(varargin{1});
                obj.month = num2str(varargin{2});
                obj.day = num2str(varargin{3});
                obj.hour = num2str(varargin{4});
                obj.minute = num2str(varargin{5});
                obj.second = num2str(varargin{6});
            else
                val = varargin{1};
                obj.year =  val(1:4);
                obj.month = val(5:6);
                obj.day = val(7:8);
                obj.hour = val(9:10);
                obj.minute = val(11:12);
                obj.second = val(13:14);
            end
        end
        function r = toString(obj)
            r = strcat(obj.year, obj.month, obj.day, obj.hour, obj.minute, obj.second);
        end
        function obj = addTime(obj, val)
            obj.second = num2str(str2num(obj.second) + str2num(val(13:14)));
            % If second is bigger or equeal to 60, add 1 minute and
            % subtract 60 seconds from second
            while str2num(obj.second) >= 60
                obj.minute = num2str(str2num(obj.minute) + 1);
                obj.second = num2str(str2num(obj.second) - 60);
            end
            obj.minute = num2str(str2num(obj.minute) + str2num(val(11:12)));
            % If minute is bigger or equeal to 60, add 1 hour and
            % subtract 60 minutes from minute
            while str2num(obj.minute) >= 60
                obj.hour = num2str(str2num(obj.hour) + 1);
                obj.minute = num2str(str2num(obj.minute) - 60);
            end
            obj.hour = num2str(str2num(obj.hour) + str2num(val(9:10)));
            % If hour is bigger or equeal to 24, add 1 day and
            % subtract 24 hours from hour
            while str2num(obj.hour) >= 24
                obj.day = num2str(str2num(obj.day) + 1);
                obj.hour = num2str(str2num(obj.hour) - 24);
            end
            obj.day = num2str(str2num(obj.day) + str2num(val(7:8)));
            % If day is bigger than the number of days in the current month, add 1 month and
            % subtract number of days from day
            while str2num(obj.day) > MyMonth.getDays(str2num(obj.month), str2num(obj.year))
                obj.day = num2str(str2num(obj.day) - MyMonth.getDays(str2num(obj.month), str2num(obj.year)));
                obj.month = num2str(str2num(obj.month) + 1); 
            end
            obj.month = num2str(str2num(obj.month) + str2num(val(5:6)));
            % If month is bigger than 12, add 1 year and
            % subtract 12 from month
            while str2num(obj.month) > 12
                obj.year = num2str(str2num(obj.year) + 1);
                obj.month = num2str(str2num(obj.month) - 12); 
            end
            
            obj.year = num2str(str2num(obj.year) + str2num(val(1:4)));
            
        end
        function obj = addHours(obj, val)
            val = num2str(val);
            if length(val) == 1
               val = ['0' val]
            end
            if length(val) ~= 2
                error('Date:addHours', 'Only a 2 digit number is allowed');
            end
            
            obj.hour = num2str(str2num(obj.hour) + str2num(val));
            % If hour is bigger or equeal to 24, add 1 day and
            % subtract 24 hours from hour
            while str2num(obj.hour) >= 24
                obj.day = num2str(str2num(obj.day) + 1);
                obj.hour = num2str(str2num(obj.hour) - 24);
            end
            % If day is bigger than the number of days in the current month, add 1 month and
            % subtract number of days from day
            while str2num(obj.day) > MyMonth.getDays(str2num(obj.month), str2num(obj.year))
                obj.day = num2str(str2num(obj.day) - MyMonth.getDays(str2num(obj.month), str2num(obj.year)));
                obj.month = num2str(str2num(obj.month) + 1); 
            end
            % If month is bigger than 12, add 1 year and
            % subtract 12 from month
            while str2num(obj.month) > 12
                obj.year = num2str(str2num(obj.year) + 1);
                obj.month = num2str(str2num(obj.month) - 12); 
            end
        end
    end
end