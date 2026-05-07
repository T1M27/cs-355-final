import { NavLink } from "react-router-dom";
import styles from "./NavBar.module.css";

export default function NavBar() {

    return (
        <>
            <nav className={styles.navBarMain}>
                <NavLink to="/" className={({isActive}) => isActive ? styles.navActive : styles.navInActive }>Home</NavLink> |
                <NavLink to="/registration" className={({isActive}) => isActive ? styles.navActive : styles.navInActive }>Register</NavLink> |
                <NavLink to="/login" className={({isActive}) => isActive ? styles.navActive : styles.navInActive }>Login</NavLink> |
                <NavLink to="/challenge/change" className={({isActive}) => isActive ? styles.navActive : styles.navInActive }>Add Challenge</NavLink>
            </nav>
        </>
    )
}