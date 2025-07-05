// src/context/UserContext.jsx
import { createContext, useContext, useState } from 'react';
import { useEffect } from 'react';

// Create the context
const UserContext = createContext();

// Create the provider
export const UserProvider = ({ children }) => {

  const [user, setUser] = useState(null);

  // ✅ Load from localStorage on mount
  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) 
    {
      try {
        setUser(JSON.parse(storedUser));
      } catch (err) {
        console.error("Failed to parse user from localStorage:", err);
        localStorage.removeItem('user'); // cleanup if invalid
      }
    }
  }, []);

  // ✅ Save to localStorage on login
  const login = (userData) => {
    setUser(userData);
    localStorage.setItem('user', JSON.stringify(userData));
  };

  // ✅ Clear localStorage on logout
  const logout = () => {
    setUser(null);
    localStorage.removeItem('user');
  };

  return (
    <UserContext.Provider value={{ user, login, logout }}>
      {children}
    </UserContext.Provider>
  );
};

// Custom hook for easier access
export const useUser = () => useContext(UserContext);
