
const DialogWrapper = ({ children, onClickOut }) => { 

    const clickOut = (e) => { 
        if (e.target === e.currentTarget) onClickOut();
    }

    return <div className="dialog" onClick={(e) => { clickOut(e) }}>
        {children}
    </div>
}

export default DialogWrapper