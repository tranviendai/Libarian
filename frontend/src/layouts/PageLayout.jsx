import Nav from '../components/shared/navbar';
//import useGlobalContext from '../contexts/GlobalContext';
import Background from '../components/shared/background';

const Layout = ({ children }) => { 
    //const { lightMode, toggleLightMode } = useGlobalContext();

    return <>
        <div className='main'>
            <Nav />
            {/* <div className={`light-mode btn ${lightMode ? '' : 'night-mode'}`} onClick={toggleLightMode}>
                <div className="label">
                    {lightMode ? 'Chế độ sáng' : 'Chế độ tối'}
                </div>
                <div className="round">
                    <div className="mini-round"></div>
                </div>
            </div> */}
            <div className='page-container'>
                <Background />
                {children}
            </div>
        </div>
    </>
}

export default Layout