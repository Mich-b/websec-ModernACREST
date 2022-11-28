package authz

default allow = false

allowedOperation = ["GET"]
allowedScheme = ["http", "https"]
allowedPermissions = ["productapi.read"]

allow  {  
  is_schema_allowed
  is_authorized
  is_operation_allowed
} 

# allowing only http and https
is_schema_allowed {
  some i 
  input.request.scheme == allowedScheme[i]
}

# allowing only certain actions
is_authorized {
  some k, j
  input.resources.requirements[j] == allowedPermissions[k]
}

# allowing only POST and GET operations
is_operation_allowed {
  some j
  input.request.method == allowedOperation[j]
}