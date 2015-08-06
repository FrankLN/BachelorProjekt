classdef BasicClass
    %BASICCLASS Summary of this class goes here
    %   Detailed explanation goes here
    
    properties
        Value
    end
    
    methods
        function r = roundOff(obj)
            [obj.Value]
            r = roundn([obj.Value],-2);
        end
        function r = multiplyBy(obj, n)
           r = [obj.Value] * n; 
        end
    end
end

