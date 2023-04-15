import { createContext, useContext, useState } from "react";

const GlobalContext = createContext();

const GlobalContextProvider = ({ children }) => { 
    const getLightModeFromStorage = () => localStorage.getItem('light-mode') !== 'night';
    const getTokenFromStorage = () => localStorage.getItem('token');
    const getRoleFromStorage = () => localStorage.getItem('role');

    const [lightMode, setLightMode] = useState(getLightModeFromStorage());
    const [token, setToken] = useState(getTokenFromStorage());
    const [role, setRole] = useState(getRoleFromStorage);

    const toggleLightMode = () => { 
        localStorage.setItem('light-mode', lightMode ? 'night' : 'day');
        setLightMode(getLightModeFromStorage());
    }
    const addToken = (newToken) => { 
        localStorage.setItem('token', newToken);
        setToken(newToken);
    }
    const removeToken = () => { 
        localStorage.removeItem('token');
        localStorage.removeItem('role');

        setToken(getTokenFromStorage);
        setRole(getRoleFromStorage);
    }

    const changeRole = (newRole) => { 
        localStorage.setItem('role', newRole);
        setRole(getRoleFromStorage);
    }

    return <GlobalContext.Provider value={{ lightMode, toggleLightMode, token, addToken, removeToken, role, changeRole }}>
        {children}
    </GlobalContext.Provider>
}

const useGlobalContext = () => useContext(GlobalContext);

export { GlobalContextProvider }
export default useGlobalContext