import { createContext, useContext, useState } from "react";

const GlobalContext = createContext();

const GlobalContextProvider = ({ children }) => { 
    const [lightMode, setLightMode] = useState(localStorage.getItem('light-mode') !== 'night');

    const toggleLightMode = () => { 
        localStorage.setItem('light-mode', lightMode ? 'night' : 'day');
        setLightMode(x => !x);
    }

    return <GlobalContext.Provider value={{lightMode, toggleLightMode}}>
        {children}
    </GlobalContext.Provider>
}

const useGlobalContext = () => useContext(GlobalContext);

export { GlobalContextProvider }
export default useGlobalContext