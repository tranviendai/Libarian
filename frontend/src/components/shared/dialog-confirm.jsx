
const ConfirmDialog = ({msg}) => { 
    return <div className="confirm-dialog">
        <p>{msg}</p>
        <div className="btns">
            <span className="yes">Có</span>
            <span className="no">Không</span>
        </div>
    </div>
}

export default ConfirmDialog;