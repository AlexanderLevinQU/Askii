import { useState } from "react";
import axios from "axios";
import styles from './Login.module.css';
import { useNavigate } from "react-router-dom";
import { useUser } from "../contexts/UserContext";

function Login()
{

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const { login } = useUser();
    const navigate = useNavigate();
    

    const handleSubmit = async (e) => 
    {
        e.preventDefault();
        let response = null;
        try 
        {
            response = await axios.post('/api/User/login', 
            {
                email: email,
                password: password,
            });

            console.log('Login success:', response.data);
        } catch (error)
        {
            console.error('Login error:', error.response?.data || error.message);
        }

        // Now you can use response outside try/catch:
        if (response && response.status === 200) 
        {
            console.log('Login success:', response.data);
            const user = await response.data;
            //get user
            login(user);
            navigate('/sessions');
        } 
        else
        {
            console.log('No valid response received.');
            setError('Invalid email or password');
        }
    }

    return (  
        <div className={styles.container}>
            <h1 className={styles.header}>Askii</h1>
            <form onSubmit={handleSubmit} className={styles.form}>
                <h2 className={styles.heading}>Login</h2>

                {error && <div style={{ color: 'red', marginBottom: '1rem' }}>{error}</div>}

                <input
                type="email"
                placeholder="Email"
                className={styles.input}
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
                />

                <input
                type="password"
                placeholder="Password"
                className={styles.input}
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
                />

                <button type="submit" className={styles.button}>
                Login
                </button>
            </form>
        </div>
    );
}

export default Login;