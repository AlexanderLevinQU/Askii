import { useState } from "react";
import styles from './Login.module.css';
import { useNavigate } from "react-router-dom";


function Login()
{

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleSubmit = (e) => 
    {
        e.preventDefault();

        // Simulated login logic (replace with real API call)
        if (email == 'test@example.com')
        {
            navigate('/sessions');
        } 
        else 
        {
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