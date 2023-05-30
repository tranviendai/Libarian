
const getErrorMsg = (err) => { 
    let errObj = err.response.data.errors;
    let msg = Object.entries(errObj)[0][1][0];

    return msg;
}

export default getErrorMsg;