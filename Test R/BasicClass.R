#########################################
# BasicClass
#
#
BasicClass <- setClass(
  # Set the name of the class
  "BasicClass",
  
  # Define the slots
  slots = c(
    x = "numeric",
    y = "numeric"
  ),
  
  # Set default values for the slots. (optional)
  prototype=list(
    x = 4,
    y = 2
  ),
  
  # Make a function that can test to see if the data is consistent.
  # This is not called if you have an initialize function defined!
  validity = function(object)
  {
    if((object@x < -100) || (object@y < -100)) {
      return("A number is less than -100")
    }
    return(TRUE)
  }
)
  
  # Create a method to round x.
  setGeneric(name="roundOff",
             def=function(theObject)
             {
               standardGeneric("roundOff")
             }
             )
  
  setMethod(f="roundOff",
            signature = "BasicClass",
            definition = function(theObject)
            {
              theObject@x <- round(theObject@x, 2)
              return(theObject)
            }
            )
  
  # Create a method to multiply y with a number n.
  setGeneric(name="multiplyBy",
             def=function(theObject, n)
             {
               standardGeneric("multiplyBy")
             }
  )
  
  setMethod(f="multiplyBy",
            signature = "BasicClass",
            definition = function(theObject, n)
            {
              theObject@y <- theObject@y * n
              return(theObject)
            }
  )
