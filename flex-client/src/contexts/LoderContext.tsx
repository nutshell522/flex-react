import { createContext, useState, useContext, ReactNode } from "react";

export interface LoaderContextType {
    setIsLoading: (isLoading: boolean) => void;
}

const LoaderContext = createContext<LoaderContextType>({
    setIsLoading: () => { },
});

const Loader: React.FC<{ isLoading: boolean }> = ({ isLoading }) => {
    return (
        isLoading && (
            <div className="loader">
                <div className="loader__content">
                    <div className="loader__spinner"></div>
                    <div className="loader__text">Loading...</div>
                </div>
            </div>
        )
    );
}

export const LoaderProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [isLoading, setIsLoading] = useState<boolean>(false);

    return (
        <LoaderContext.Provider value={{ setIsLoading }}>
            <Loader isLoading={isLoading} />
            {children}
        </LoaderContext.Provider>
    );
};

// eslint-disable-next-line react-refresh/only-export-components
export const useLoaderContext = () => {
    const context = useContext(LoaderContext);
    if (!context) {
        throw new Error("useLoaderContext must be used within a LoaderProvider");
    }
    return context;
}