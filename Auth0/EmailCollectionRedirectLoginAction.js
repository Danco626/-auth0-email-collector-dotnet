/**
* Handler that will be called during the execution of a PostLogin flow.
*
* @param {Event} event - Details about the user and the context in which they are logging in.
* @param {PostLoginAPI} api - Interface whose methods can be used to change the behavior of the login.
*/
exports.onExecutePostLogin = async (event, api) => {  
    if(event.user.app_metadata?.altEmail) {
      console.log("setting email2 claim")
      api.idToken.setCustomClaim("altEmail", event.user.app_metadata?.altEmail);
      api.accessToken.setCustomClaim("altEmail", event.user.app_metadata?.altEmail);
    } 

    if (!event.client.metadata?.skipRedirect) {
      if (!event.user.email && !event.user.app_metadata?.altEmail) {
        api.redirect.sendUserTo("http://localhost:3000/emailprompt");          
      }
    }
      
  };
  
  
  /**
  * Handler that will be invoked when this action is resuming after an external redirect. If your
  * onExecutePostLogin function does not perform a redirect, this function can be safely ignored.
  *
  * @param {Event} event - Details about the user and the context in which they are logging in.
  * @param {PostLoginAPI} api - Interface whose methods can be used to change the behavior of the login.
  */
   exports.onContinuePostLogin = async (event, api) => {
     console.log(event.user.app_metadata?.altEmail)
      api.idToken.setCustomClaim("altEmail", event.user.app_metadata?.altEmail);
      api.accessToken.setCustomClaim("altEmail", event.user.app_metadata?.altEmail)  
   };